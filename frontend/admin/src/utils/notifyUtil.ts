import { notification } from 'antd';

class NotifyUtil {
    static openNotification(
        type: 'success' | 'info' | 'warning' | 'error',
        message: string,
        description?: string,
        duration: number = 4.5
    ): void {
        notification[type]({
            message,
            description,
            duration,
            placement: 'topRight',
            showProgress: true,
            pauseOnHover: true,
            className: 'base-notification'
        });
    }

    static success(message: string, description?: string, duration?: number): void {
        NotifyUtil.openNotification('success', message, description, duration);
    }

    static info(message: string, description?: string, duration?: number): void {
        NotifyUtil.openNotification('info', message, description, duration);
    }

    static warning(message: string, description?: string, duration?: number): void {
        NotifyUtil.openNotification('warning', message, description, duration);
    }

    static error(message: string, description?: string, duration?: number): void {
        NotifyUtil.openNotification('error', message, description, duration);
    }
}

export default NotifyUtil;
