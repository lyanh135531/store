import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const navigate = useNavigate();

const axiosInstance = axios.create({
  baseURL: process.env.BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json'
  }
});

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      navigate('/login');
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
