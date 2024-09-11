import axiosInstance from '@/utils/axiosConfig';
import { AxiosHeaders, AxiosRequestConfig, AxiosResponse, Method } from 'axios';

export class ApiUtil {
    static Axios<T>(
        method: Method,
        url: string,
        params?: unknown,
        data?: unknown,
        headers?: AxiosHeaders
    ): Promise<AxiosResponse<T>> {
        const config: AxiosRequestConfig = {
            params,
            data,
            headers,
            withCredentials: true
        };
        switch (method.toUpperCase()) {
            case 'GET':
                return axiosInstance.get<T>(url, config);

            case 'POST':
                return axiosInstance.post<T>(url, data, config);

            case 'PUT':
                return axiosInstance.put<T>(url, data, config);

            case 'DELETE':
                return axiosInstance.delete<T>(url, config);

            default:
                return Promise.reject(new Error(`Unsupported method: ${method}`));
        }
    }
}
