class AppsinRichTextEditor {
    constructor(containerId, options = {}) {
        // 默认配置
        this.defaults = {
            showHtmlOutput: false,
            minHeight: 100,
            maxHeight: null,
            initialHeight: null,
            toolbar: {
                fontSize: true,
                bold: true,
                italic: true,
                underline: true,
                align: true,
                lists: true,
                image: true
            },
            // 图片上传配置
            image: {
                maxSize: 1024 * 1024, // 1MB
                allowedTypes: ['image/jpeg', 'image/png', 'image/gif']
            },
            // 弹窗层级配置
            zIndex: 99999
        };

        // 合并用户配置
        this.options = { ...this.defaults, ...options };
        this.options.image = { ...this.defaults.image, ...(options.image || {}) };

        // 获取容器元素
        this.container = document.getElementById(containerId);
        if (!this.container) {
            throw new Error(`Container with id "${containerId}" not found`);
        }

        // 设置容器样式
        this.container.style.position = 'relative';
        this.container.style.overflow = 'hidden';

        // 初始化变量
        this.selectedImage = null;
        this.isModalOpen = false;

        // 创建编辑器结构
        this.init();

        // 监听窗口大小变化
        window.addEventListener('resize', () => this.adjustEditorSize());

        // 初始调整大小
        this.adjustEditorSize();
    }

    // 初始化编辑器
    init() {
        // 创建编辑器主容器
        this.editorElement = document.createElement('div');
        this.editorElement.className = 'appsin-rich-text-editor';

        // 创建工具栏
        this.createToolbar();

        // 创建编辑区域
        this.createEditorArea();

        // 创建图片大小调整弹窗
        this.createImageSizeModal();

        // 将编辑器添加到容器
        this.container.appendChild(this.editorElement);

        // 绑定事件
        this.bindEvents();
    }

    // 调整编辑器大小以适应容器
    adjustEditorSize() {
        this.editorElement.style.width = '100%';
        this.editorElement.style.height = '100%';

        if (this.options.minHeight) {
            this.container.style.minHeight = `${this.options.minHeight}px`;
        }
    }

    // 创建工具栏
    createToolbar() {
        const toolbar = document.createElement('div');
        toolbar.className = 'appsin-editor-toolbar';

        // 字体大小选择
        if (this.options.toolbar.fontSize) {
            const fontSizeContainer = document.createElement('div');
            fontSizeContainer.className = 'appsin-font-size-control';

            const fontSizeSelect = document.createElement('select');
            fontSizeSelect.className = 'appsin-font-size-select';
            fontSizeSelect.id = 'appsin-rte-font-size';

            const fontSizeOptions = [
                { value: '', text: 'Font Size' },
                { value: '1', text: 'Tiny' },
                { value: '2', text: 'Small' },
                { value: '3', text: 'Normal' },
                { value: '4', text: 'Medium' },
                { value: '5', text: 'Large' },
                { value: '6', text: 'Larger' },
                { value: '7', text: 'Huge' }
            ];

            fontSizeOptions.forEach(option => {
                const opt = document.createElement('option');
                opt.value = option.value;
                opt.textContent = option.text;
                if (option.value === '3') opt.selected = true;
                fontSizeSelect.appendChild(opt);
            });

            fontSizeContainer.appendChild(fontSizeSelect);
            toolbar.appendChild(fontSizeContainer);
            toolbar.appendChild(this.createSeparator());
        }

        // 文本样式按钮
        if (this.options.toolbar.bold) {
            toolbar.appendChild(this.createToolbarButton('bold', 'Bold', 'fas fa-bold'));
        }
        if (this.options.toolbar.italic) {
            toolbar.appendChild(this.createToolbarButton('italic', 'Italic', 'fas fa-italic'));
        }
        if (this.options.toolbar.underline) {
            toolbar.appendChild(this.createToolbarButton('underline', 'Underline', 'fas fa-underline'));
        }

        if (this.options.toolbar.bold || this.options.toolbar.italic || this.options.toolbar.underline) {
            toolbar.appendChild(this.createSeparator());
        }

        // 对齐按钮
        if (this.options.toolbar.align) {
            toolbar.appendChild(this.createToolbarButton('justifyLeft', 'Align Left', 'fas fa-align-left'));
            toolbar.appendChild(this.createToolbarButton('justifyCenter', 'Align Center', 'fas fa-align-center'));
            toolbar.appendChild(this.createToolbarButton('justifyRight', 'Align Right', 'fas fa-align-right'));
            toolbar.appendChild(this.createSeparator());
        }

        // 列表按钮
        if (this.options.toolbar.lists) {
            toolbar.appendChild(this.createToolbarButton('insertUnorderedList', 'Bullet List', 'fas fa-list-ul'));
            toolbar.appendChild(this.createToolbarButton('insertOrderedList', 'Numbered List', 'fas fa-list-ol'));
            toolbar.appendChild(this.createSeparator());
        }

        // 图片上传
        if (this.options.toolbar.image) {
            const uploadContainer = document.createElement('div');
            uploadContainer.className = 'appsin-upload-container';

            const uploadButton = document.createElement('button');
            uploadButton.className = 'appsin-toolbar-btn';
            uploadButton.title = 'Upload Image';
            uploadButton.id = 'appsin-rte-upload-btn';

            const icon = document.createElement('i');
            icon.className = 'fas fa-image';
            uploadButton.appendChild(icon);

            const fileInput = document.createElement('input');
            fileInput.type = 'file';
            fileInput.className = 'appsin-image-upload-input';
            fileInput.accept = 'image/jpeg, image/png, image/gif';
            fileInput.id = 'appsin-rte-image-upload';

            uploadContainer.appendChild(uploadButton);
            uploadContainer.appendChild(fileInput);
            toolbar.appendChild(uploadContainer);

            // 调整图片大小按钮
            const resizeButton = document.createElement('button');
            resizeButton.className = 'appsin-toolbar-btn';
            resizeButton.id = 'appsin-rte-resize-image';
            resizeButton.title = 'Resize Image';
            resizeButton.disabled = true;

            const resizeIcon = document.createElement('i');
            resizeIcon.className = 'fas fa-expand-arrows-alt';
            resizeButton.appendChild(resizeIcon);

            toolbar.appendChild(resizeButton);
        }

        this.editorElement.appendChild(toolbar);
    }

    // 创建工具栏按钮
    createToolbarButton(command, title, iconClass) {
        const button = document.createElement('button');
        button.className = 'appsin-toolbar-btn';
        button.dataset.command = command;
        button.title = title;

        const icon = document.createElement('i');
        icon.className = iconClass;
        button.appendChild(icon);

        return button;
    }

    // 创建分隔线
    createSeparator() {
        const separator = document.createElement('div');
        separator.className = 'appsin-separator';
        return separator;
    }

    // 创建编辑区域
    createEditorArea() {
        const wrapper = document.createElement('div');
        wrapper.className = 'appsin-editor-wrapper';

        const content = document.createElement('div');
        content.className = 'appsin-editor-content';
        content.contentEditable = true;
        content.id = 'appsin-rte-editor-content';

        if (this.options.initialHeight) {
            content.style.height = `${this.options.initialHeight}px`;
        }

        wrapper.appendChild(content);
        this.editorElement.appendChild(wrapper);
    }

    // 创建图片大小调整弹窗
    createImageSizeModal() {
        const modalBackdrop = document.createElement('div');
        modalBackdrop.className = 'appsin-rte-modal-backdrop';
        modalBackdrop.id = 'appsin-rte-image-size-modal-backdrop';
        modalBackdrop.style.zIndex = this.options.zIndex;

        const modal = document.createElement('div');
        modal.className = 'appsin-rte-modal';
        // 添加modal-specific类名用于样式和事件目标识别
        modal.classList.add('appsin-modal-dialog');

        // 弹窗头部
        const modalHeader = document.createElement('div');
        modalHeader.className = 'appsin-rte-modal-header';

        const modalTitle = document.createElement('h5');
        modalTitle.className = 'appsin-rte-modal-title';
        modalTitle.textContent = 'Resize Image';

        const closeButton = document.createElement('button');
        closeButton.className = 'appsin-rte-modal-close';
        closeButton.id = 'appsin-rte-modal-close';
        closeButton.innerHTML = '&times;';

        modalHeader.appendChild(modalTitle);
        modalHeader.appendChild(closeButton);

        // 弹窗内容
        const modalBody = document.createElement('div');
        modalBody.className = 'appsin-rte-modal-body';

        // 宽度输入
        const widthGroup = document.createElement('div');
        widthGroup.className = 'appsin-rte-form-group';

        const widthLabel = document.createElement('label');
        widthLabel.className = 'appsin-rte-form-label';
        widthLabel.htmlFor = 'appsin-rte-image-width';
        widthLabel.textContent = 'Width (pixels)';

        const widthInput = document.createElement('input');
        widthInput.type = 'number';
        widthInput.id = 'appsin-rte-image-width';
        widthInput.className = 'appsin-rte-form-control';
        widthInput.min = 10;
        widthInput.step = 1;
        // 确保输入框可以获得焦点
        widthInput.tabIndex = 0;
        widthInput.autocomplete = 'off';

        widthGroup.appendChild(widthLabel);
        widthGroup.appendChild(widthInput);

        // 高度输入
        const heightGroup = document.createElement('div');
        heightGroup.className = 'appsin-rte-form-group';

        const heightLabel = document.createElement('label');
        heightLabel.className = 'appsin-rte-form-label';
        heightLabel.htmlFor = 'appsin-rte-image-height';
        heightLabel.textContent = 'Height (pixels)';

        const heightInput = document.createElement('input');
        heightInput.type = 'number';
        heightInput.id = 'appsin-rte-image-height';
        heightInput.className = 'appsin-rte-form-control';
        heightInput.min = 10;
        heightInput.step = 1;
        // 确保输入框可以获得焦点
        heightInput.tabIndex = 0;
        heightInput.autocomplete = 'off';

        heightGroup.appendChild(heightLabel);
        heightGroup.appendChild(heightInput);

        // 保持宽高比选项
        const aspectRatioContainer = document.createElement('div');
        aspectRatioContainer.className = 'appsin-aspect-ratio-container';

        const ratioCheckbox = document.createElement('input');
        ratioCheckbox.type = 'checkbox';
        ratioCheckbox.id = 'appsin-rte-preserve-aspect-ratio';
        ratioCheckbox.className = 'appsin-form-check-input';
        ratioCheckbox.checked = true;
        ratioCheckbox.tabIndex = 0;

        const ratioLabel = document.createElement('label');
        ratioLabel.className = 'appsin-form-check-label';
        ratioLabel.htmlFor = 'appsin-rte-preserve-aspect-ratio';
        ratioLabel.textContent = 'Preserve aspect ratio';

        aspectRatioContainer.appendChild(ratioCheckbox);
        aspectRatioContainer.appendChild(ratioLabel);

        modalBody.appendChild(widthGroup);
        modalBody.appendChild(heightGroup);
        modalBody.appendChild(aspectRatioContainer);

        // 弹窗底部
        const modalFooter = document.createElement('div');
        modalFooter.className = 'appsin-rte-modal-footer';

        const cancelButton = document.createElement('button');
        cancelButton.className = 'appsin-btn appsin-btn-secondary';
        cancelButton.id = 'appsin-rte-modal-cancel';
        cancelButton.textContent = 'Cancel';
        cancelButton.tabIndex = 0;

        const applyButton = document.createElement('button');
        applyButton.className = 'appsin-btn appsin-btn-primary';
        applyButton.id = 'appsin-rte-apply-size-changes';
        applyButton.textContent = 'Apply';
        applyButton.tabIndex = 0;

        modalFooter.appendChild(cancelButton);
        modalFooter.appendChild(applyButton);

        // 组装弹窗
        modal.appendChild(modalHeader);
        modal.appendChild(modalBody);
        modal.appendChild(modalFooter);
        modalBackdrop.appendChild(modal);

        // 添加到body
        document.body.appendChild(modalBackdrop);
    }

    // 绑定事件处理程序
    bindEvents() {
        // 工具栏按钮点击事件
        document.querySelectorAll('.appsin-toolbar-btn[data-command]').forEach(button => {
            button.addEventListener('click', (e) => {
                const command = e.currentTarget.dataset.command;
                document.execCommand(command, false, null);
                this.getEditorContent().focus();
            });
        });

        // 字体大小选择事件
        const fontSizeSelect = document.getElementById('appsin-rte-font-size');
        if (fontSizeSelect) {
            fontSizeSelect.addEventListener('change', (e) => {
                const fontSize = e.target.value;
                if (fontSize) {
                    document.execCommand('fontSize', false, fontSize);
                    this.getEditorContent().focus();
                }
            });
        }

        // 编辑区域图片点击事件
        this.getEditorContent().addEventListener('click', (e) => {
            // 如果弹窗打开，不处理编辑器内的点击
            if (this.isModalOpen) return;

            if (e.target.tagName === 'IMG') {
                e.stopPropagation();
                this.deselectImage();
                this.selectedImage = e.target;
                this.selectedImage.classList.add('appsin-selected');
                document.getElementById('appsin-rte-resize-image').disabled = false;
            } else {
                this.deselectImage();
                document.getElementById('appsin-rte-resize-image').disabled = true;
            }
        });

        // 编辑区域键盘事件 - 修复焦点问题
        this.getEditorContent().addEventListener('keydown', (e) => {
            if (e.key === 'ArrowLeft' && this.selectedImage) {
                this.deselectImage();
                document.getElementById('appsin-rte-resize-image').disabled = true;

                const selection = window.getSelection();
                const range = document.createRange();
                range.setStartBefore(this.selectedImage);
                range.setEndBefore(this.selectedImage);
                selection.removeAllRanges();
                selection.addRange(range);

                e.preventDefault();
            }
        });

        // 调整图片大小按钮点击事件
        const resizeButton = document.getElementById('appsin-rte-resize-image');
        if (resizeButton) {
            resizeButton.addEventListener('click', () => {
                if (this.selectedImage) {
                    const width = this.selectedImage.style.width ?
                        parseInt(this.selectedImage.style.width) :
                        this.selectedImage.offsetWidth;
                    const height = this.selectedImage.style.height ?
                        parseInt(this.selectedImage.style.height) :
                        this.selectedImage.offsetHeight;

                    const widthInput = document.getElementById('appsin-rte-image-width');
                    const heightInput = document.getElementById('appsin-rte-image-height');

                    widthInput.value = width;
                    heightInput.value = height;

                    this.showImageSizeModal();

                    // 显示弹窗后立即设置焦点到宽度输入框
                    setTimeout(() => {
                        widthInput.focus();
                        // 选中输入框内容以便直接输入新值
                        widthInput.select();
                    }, 100);
                }
            });
        }

        // 应用图片大小更改
        document.getElementById('appsin-rte-apply-size-changes').addEventListener('click', () => {
            if (this.selectedImage) {
                const newWidth = parseInt(document.getElementById('appsin-rte-image-width').value);
                const newHeight = parseInt(document.getElementById('appsin-rte-image-height').value);
                const preserveRatio = document.getElementById('appsin-rte-preserve-aspect-ratio').checked;

                if (!isNaN(newWidth) && newWidth > 0) {
                    if (preserveRatio) {
                        const ratio = this.selectedImage.offsetHeight / this.selectedImage.offsetWidth;
                        this.selectedImage.style.width = newWidth + 'px';
                        this.selectedImage.style.height = (newWidth * ratio) + 'px';
                        document.getElementById('appsin-rte-image-height').value = Math.round(newWidth * ratio);
                    } else if (!isNaN(newHeight) && newHeight > 0) {
                        this.selectedImage.style.width = newWidth + 'px';
                        this.selectedImage.style.height = newHeight + 'px';
                    }

                    this.hideImageSizeModal();
                } else {
                    alert('Please enter valid dimensions');
                    // 错误提示后重新聚焦到输入框
                    document.getElementById('appsin-rte-image-width').focus();
                }
            }
        });

        // 关闭弹窗按钮事件
        document.getElementById('appsin-rte-modal-close').addEventListener('click', () => {
            this.hideImageSizeModal();
        });

        document.getElementById('appsin-rte-modal-cancel').addEventListener('click', () => {
            this.hideImageSizeModal();
        });

        // 阻止事件冒泡，确保弹窗内元素可以获得焦点
        document.querySelector('.appsin-modal-dialog').addEventListener('click', (e) => {
            e.stopPropagation();
        });

        // 宽度输入处理
        const widthInput = document.getElementById('appsin-rte-image-width');
        widthInput.addEventListener('input', (e) => {
            // 过滤非数字字符
            e.target.value = e.target.value.replace(/[^0-9]/g, '');

            if (document.getElementById('appsin-rte-preserve-aspect-ratio').checked && this.selectedImage) {
                const newWidth = parseInt(e.target.value) || 0;
                if (newWidth > 0) {
                    const ratio = this.selectedImage.offsetHeight / this.selectedImage.offsetWidth;
                    document.getElementById('appsin-rte-image-height').value = Math.round(newWidth * ratio);
                }
            }
        });

        // 高度输入处理
        const heightInput = document.getElementById('appsin-rte-image-height');
        heightInput.addEventListener('input', (e) => {
            // 过滤非数字字符
            e.target.value = e.target.value.replace(/[^0-9]/g, '');

            if (document.getElementById('appsin-rte-preserve-aspect-ratio').checked && this.selectedImage) {
                const newHeight = parseInt(e.target.value) || 0;
                if (newHeight > 0) {
                    const ratio = this.selectedImage.offsetWidth / this.selectedImage.offsetHeight;
                    document.getElementById('appsin-rte-image-width').value = Math.round(newHeight * ratio);
                }
            }
        });

        // 为输入框添加聚焦事件，确保可以手动聚焦
        widthInput.addEventListener('focus', () => {
            widthInput.classList.add('focused');
        });

        heightInput.addEventListener('focus', () => {
            heightInput.classList.add('focused');
        });

        // 图片上传按钮点击事件
        const uploadButton = document.getElementById('appsin-rte-upload-btn');
        const imageUpload = document.getElementById('appsin-rte-image-upload');
        if (uploadButton && imageUpload) {
            uploadButton.addEventListener('click', (e) => {
                e.preventDefault();
                imageUpload.click();
            });
        }

        // 图片上传处理
        if (imageUpload) {
            imageUpload.addEventListener('change', (e) => {
                const file = e.target.files[0];
                if (!file) return;

                // 验证文件类型
                if (!this.options.image.allowedTypes.includes(file.type)) {
                    alert(`Only the following image formats are supported: ${this.options.image.allowedTypes.map(type => type.split('/')[1].toUpperCase()).join(', ')}`);
                    e.target.value = '';
                    return;
                }

                // 验证文件大小
                const isOversized = file.size > this.options.image.maxSize;
                if (isOversized) {
                    if (!confirm(`Image size exceeds ${(this.options.image.maxSize / 1024).toFixed(0)}KB. It will be automatically compressed before upload. Continue?`)) {
                        e.target.value = '';
                        return;
                    }
                }

                const reader = new FileReader();
                const editorContent = this.getEditorContent();

                reader.onload = (event) => {
                    const img = new Image();
                    img.onload = () => {
                        if (isOversized) {
                            const compressedDataUrl = this.compressImage(img, file.type);
                            this.insertImageToEditor(compressedDataUrl, editorContent);
                            alert(`Image has been compressed to under ${(this.options.image.maxSize / 1024).toFixed(0)}KB`);
                        } else {
                            this.insertImageToEditor(event.target.result, editorContent);
                        }
                    };

                    img.src = event.target.result;
                };

                reader.onerror = () => {
                    alert('Failed to read image. Please try again.');
                };

                reader.readAsDataURL(file);
                e.target.value = '';
            })
        }

        // 添加键盘导航支持
        document.getElementById('appsin-rte-image-size-modal-backdrop').addEventListener('keydown', (e) => {
            // ESC键关闭弹窗
            if (e.key === 'Escape') {
                this.hideImageSizeModal();
            }

            // Enter键应用更改
            if (e.key === 'Enter' && this.isModalOpen) {
                document.getElementById('appsin-rte-apply-size-changes').click();
            }
        });
    }

    /**
     * 压缩图片至指定大小以下
     * @param {HTMLImageElement} img - 图片对象
     * @param {string} mimeType - 图片类型
     * @returns {string} 压缩后的图片DataURL
     */
    compressImage(img, mimeType) {
        const canvas = document.createElement('canvas');
        const ctx = canvas.getContext('2d');

        const maxWidth = 1200;
        const maxHeight = 1200;
        let width = img.width;
        let height = img.height;

        if (width > maxWidth) {
            height = (height * maxWidth) / width;
            width = maxWidth;
        }
        if (height > maxHeight) {
            width = (width * maxHeight) / height;
            height = maxHeight;
        }

        canvas.width = width;
        canvas.height = height;
        ctx.drawImage(img, 0, 0, width, height);

        let quality = 0.9;
        let dataUrl = canvas.toDataURL(mimeType, quality);

        while (this.dataUrlToSize(dataUrl) > this.options.image.maxSize && quality > 0.1) {
            quality -= 0.1;
            dataUrl = canvas.toDataURL(mimeType, quality);
        }

        return dataUrl;
    }

    /**
     * 将DataURL转换为字节大小
     * @param {string} dataUrl - 图片的DataURL
     * @returns {number} 字节大小
     */
    dataUrlToSize(dataUrl) {
        const base64Str = dataUrl.split(',')[1];
        return (base64Str.length * 3) / 4;
    }

    /**
     * 将图片插入到编辑器中
     * @param {string} dataUrl - 图片的DataURL
     * @param {HTMLElement} editorContent - 编辑器内容区域
     */
    insertImageToEditor(dataUrl, editorContent) {
        editorContent.focus();

        const selection = window.getSelection();
        let range;

        if (selection.rangeCount > 0) {
            range = selection.getRangeAt(0);
        } else {
            range = document.createRange();
            range.selectNodeContents(editorContent);
            range.collapse(false);
        }

        const img = document.createElement('img');
        img.src = dataUrl;
        img.style.maxWidth = '100%';
        img.className = 'appsin-uploaded-image';

        try {
            range.insertNode(img);

            const newRange = document.createRange();
            newRange.setStartAfter(img);
            newRange.setEndAfter(img);
            selection.removeAllRanges();
            selection.addRange(newRange);

            this.deselectImage();
            this.selectedImage = img;
            this.selectedImage.classList.add('appsin-selected');
            document.getElementById('appsin-rte-resize-image').disabled = false;
        } catch (error) {
            console.error('Error inserting image:', error);
            editorContent.appendChild(img);
            this.selectedImage = img;
            this.selectedImage.classList.add('appsin-selected');
            document.getElementById('appsin-rte-resize-image').disabled = false;
        }
    }

    // 获取编辑区域元素
    getEditorContent() {
        return document.getElementById('appsin-rte-editor-content');
    }

    // 取消图片选择
    deselectImage() {
        document.querySelectorAll('.appsin-editor-content img.appsin-selected').forEach(img => {
            img.classList.remove('appsin-selected');
        });
        this.selectedImage = null;
    }

    // 显示图片大小调整弹窗
    showImageSizeModal() {
        const modal = document.getElementById('appsin-rte-image-size-modal-backdrop');
        modal.style.display = 'flex';
        document.body.style.overflow = 'hidden';
        this.isModalOpen = true;
    }

    // 隐藏图片大小调整弹窗
    hideImageSizeModal() {
        const modal = document.getElementById('appsin-rte-image-size-modal-backdrop');
        modal.style.display = 'none';
        document.body.style.overflow = '';
        this.isModalOpen = false;
        // 恢复焦点到编辑器
        this.getEditorContent().focus();
    }

    // 获取编辑器内容（HTML格式）
    getContent() {
        return this.getEditorContent().innerHTML;
    }

    // 设置编辑器内容（HTML格式）
    setContent(html) {
        this.getEditorContent().innerHTML = html;
    }

    // 清空编辑器内容
    clearContent() {
        this.getEditorContent().innerHTML = '';
    }
}
