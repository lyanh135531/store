import { AxiosHeaders, AxiosRequestConfig, AxiosResponse, Method } from 'axios';
import axiosInstance from './axiosConfig';

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
      headers
    };
    switch (method.toUpperCase()) {
      case 'GET':
        return axiosInstance.get<T>(url, config);

      case 'POST':
        return axiosInstance.post<T>(url, data, config);

      case 'PUT':
        return axiosInstance.put<T>(url, data, config);

      case 'DELETE':
        return axiosInstance.delete<T>(url);

      default:
        return Promise.reject(new Error(`Unsupported method: ${method}`));
    }
  }
}
