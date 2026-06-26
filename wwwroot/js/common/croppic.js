let cropper;
const CROP_ASPECT_RATIO = 2 / 3;
let croppedImgData = '';
let scaleXValue = 1;
let scaleYValue = 1;

// DOM 元素
const uploadZone = document.getElementById('uploadZone');
const imageInput = document.getElementById('imageInput');
const cropperWrapper = document.getElementById('cropperWrapper');
const image = document.getElementById('appsinImage');
const cropBtn = document.getElementById('cropBtn');

// 按钮元素
const btnRotateLeft = document.getElementById('btnRotateLeft');
const btnRotateRight = document.getElementById('btnRotateRight');
const btnScaleX = document.getElementById('btnScaleX');
const btnScaleY = document.getElementById('btnScaleY');
const btnReset = document.getElementById('btnReset');

// Ensure crop button disabled until cropper ready
if (cropBtn) cropBtn.disabled = true;

// 文件上传处理
imageInput.addEventListener('change', function (e) {
    const file = e.target.files[0];
    if (!file) return;

    if (!file.type.match(/^image\/(jpeg|png)$/)) {
        alert('Please upload images in JPG or PNG format！');
        return;
    }

    if (file.size > 100 * 1024 * 1024) {
        alert('The image size cannot exceed 100MB');
        return;
    }

    const reader = new FileReader();
    reader.onload = function (event) {
        uploadZone.classList.add('hidden');
        cropperWrapper.classList.add('active');
        // don't enable cropBtn here — wait until cropper is fully initialized in ready()

        image.src = event.target.result;

        if (cropper) {
            cropper.destroy();
            cropper = null;
            if (cropBtn) cropBtn.disabled = true;
        }

        image.onload = function () {
            setTimeout(() => {
                initCropper();
            }, 100);
        };

        if (image.complete && image.naturalWidth !== 0) {
            setTimeout(() => {
                initCropper();
            }, 100);
        }
    };
    reader.readAsDataURL(file);
});

// 初始化 Cropper
function initCropper() {
    image.classList.add('cropper-active');

    cropper = new Cropper(image, {
        aspectRatio: CROP_ASPECT_RATIO,
        viewMode: 1,
        dragMode: 'move',
        autoCropArea: 0.6,
        restore: false,
        guides: true,
        center: true,
        highlight: false,
        cropBoxMovable: true,
        cropBoxResizable: true,
        toggleDragModeOnDblclick: false,
        minCanvasWidth: 100,
        minCanvasHeight: 100,
        minCropBoxWidth: 100,
        minCropBoxHeight: 100,
        ready: function () {
            const containerData = cropper.getContainerData();
            const canvasData = cropper.getCanvasData();

            // 计算合适的缩放比例，使画布适应容器
            const scaleX = (containerData.width * 0.9) / canvasData.naturalWidth;
            const scaleY = (containerData.height * 0.9) / canvasData.naturalHeight;
            const scale = Math.min(scaleX, scaleY, 1);

            // 如果图片太大，进行缩放
            if (scale < 1) {
                cropper.zoomTo(scale);
            }

            // 设置裁剪框大小
            const targetRatio = CROP_ASPECT_RATIO;
            let cropBoxWidth = Math.min(300, containerData.width * 0.6);
            let cropBoxHeight = cropBoxWidth / targetRatio;

            if (cropBoxHeight > containerData.height * 0.8) {
                cropBoxHeight = containerData.height * 0.8;
                cropBoxWidth = cropBoxHeight * targetRatio;
            }

            cropper.setCropBoxData({
                width: cropBoxWidth,
                height: cropBoxHeight,
                left: (containerData.width - cropBoxWidth) / 2,
                top: (containerData.height - cropBoxHeight) / 2
            });

            // enable crop button only when cropper is fully ready
            if (cropBtn) cropBtn.disabled = false;
        },
        crop: function (event) {

        }
    });
}

// 绑定按钮事件
btnRotateLeft.addEventListener('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    if (cropper) {
        cropper.rotate(-90);
    }
});

btnRotateRight.addEventListener('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    if (cropper) {
        cropper.rotate(90);
    }
});

btnScaleX.addEventListener('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    if (cropper) {
        scaleXValue = -scaleXValue;
        cropper.scaleX(scaleXValue);
    }
});

btnScaleY.addEventListener('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    if (cropper) {
        scaleYValue = -scaleYValue;
        cropper.scaleY(scaleYValue);
    }
});

btnReset.addEventListener('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    if (cropper) {
        cropper.reset();
        scaleXValue = 1;
        scaleYValue = 1;
    }
});

// 拖拽上传支持
['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
    uploadZone.addEventListener(eventName, preventDefaults, false);
});

function preventDefaults(e) {
    e.preventDefault();
    e.stopPropagation();
}

['dragenter', 'dragover'].forEach(eventName => {
    uploadZone.addEventListener(eventName, highlight, false);
});

['dragleave', 'drop'].forEach(eventName => {
    uploadZone.addEventListener(eventName, unhighlight, false);
});

function highlight(e) {
    uploadZone.classList.add('dragover');
}

function unhighlight(e) {
    uploadZone.classList.remove('dragover');
}

uploadZone.addEventListener('drop', handleDrop, false);

function handleDrop(e) {
    const dt = e.dataTransfer;
    const files = dt.files;

    const dataTransfer = new DataTransfer();
    for (let i = 0; i < files.length; i++) {
        dataTransfer.items.add(files[i]);
    }
    imageInput.files = dataTransfer.files;

    const event = new Event('change', { bubbles: true });
    imageInput.dispatchEvent(event);
}

// Helper: try to get cropped canvas with retries
function getCroppedCanvasWithRetry(options, maxAttempts = 20, delay = 500) {
    return new Promise((resolve, reject) => {
        let attempts = 0;
        function tryGet() {
            if (!cropper) return reject(new Error('Cropper not initialized'));
            try {
                const canvas = cropper.getCroppedCanvas(options);
                if (canvas) return resolve(canvas);
            } catch (err) {
                // ignore and retry
                console.warn('getCroppedCanvas threw, will retry', err);
            }
            attempts++;
            if (attempts >= maxAttempts) {
                return reject(new Error('Unable to get cropped canvas after retries'));
            }
            setTimeout(tryGet, delay);
        }
        tryGet();
    });
}

// 裁剪按钮点击事件
cropBtn.addEventListener('click', async function (e) {
    e.preventDefault();
    e.stopPropagation();

    if (!cropper) {
        console.warn('Cropper not initialized yet.');
        return;
    }

    // Visual feedback: change button text and show dots
    const originalHtml = cropBtn.innerHTML;
    let dots = 0;
    let dotsInterval = null;
    function startFeedback(text) {
        cropBtn.disabled = true;
        cropBtn.innerHTML = text;
        dotsInterval = setInterval(() => {
            dots = (dots + 1) % 4;
            cropBtn.innerHTML = text + '.'.repeat(dots);
        }, 400);
    }
    function stopFeedback() {
        if (dotsInterval) clearInterval(dotsInterval);
        dotsInterval = null;
        cropBtn.innerHTML = originalHtml;
        cropBtn.disabled = false;
    }

    startFeedback('Preparing');

    try {
        const canvas = await getCroppedCanvasWithRetry({
            width: 300,
            height: 500,
            fillColor: '#fff',
            imageSmoothingEnabled: true,
            imageSmoothingQuality: 'high',
        }, 20, 500);

        if (!canvas) throw new Error('getCroppedCanvas returned null');

        //The final cropped image data in Base64 format
        croppedImgData = canvas.toDataURL('image/jpeg', 0.9);
        $("#imgAvatar").attr("src", croppedImgData);
        $("#cropper").addClass("invisible");

    } catch (err) {
        console.error('Failed to get cropped canvas', err);
        alert('Unable to generate cropped image. Please try again.');
    } finally {
        stopFeedback();
    }

});

let resizeTimer;
window.addEventListener('resize', function () {
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(() => {
        if (cropper) {
            cropper.resize();
        }
    }, 250);
});