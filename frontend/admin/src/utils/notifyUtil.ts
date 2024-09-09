import { notification } from 'antd';
import { t } from 'i18next';

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
            placement: 'bottomRight',
            showProgress: true,
            pauseOnHover: true,
            className: 'base-notification'
        });
    }

    static success(message: string, description?: string, duration?: number): void {
        NotifyUtil.openNotification('success', t(message), t(description || ''), duration);
    }

    static info(message: string, description?: string, duration?: number): void {
        NotifyUtil.openNotification('info', t(message), t(description || ''), duration);
    }

    static warning(message: string, description?: string, duration?: number): void {
        NotifyUtil.openNotification('warning', t(message), t(description || ''), duration);
    }

    static error(message: string, description?: string, duration?: number): void {
        NotifyUtil.openNotification('error', t(message), t(description || ''), duration);
    }
}

export default NotifyUtil;
