import axios from 'axios';
import type { AxiosRequestConfig, AxiosResponse } from 'axios';
import { Message, Modal } from '@arco-design/web-vue';
import { useUserStore } from '@/store';
import { getToken } from '@/utils/auth';
import { isString } from '@/utils/is';

export interface HttpResponse<T = unknown> {
  data: T;
  statusCode: number;
  succeeded: boolean;
  timestamp: number;
  errors: object | string | null;
}

if (import.meta.env.VITE_API_BASE_URL) {
  axios.defaults.baseURL = import.meta.env.VITE_API_BASE_URL;
}

axios.interceptors.request.use(
  (config: AxiosRequestConfig) => {
    // let each request carry token
    // this example using the JWT token
    // Authorization is a custom headers key
    // please modify it according to the actual situation
    const token = getToken();
    if (token) {
      if (!config.headers) {
        config.headers = {};
      }
      config.headers.Authorization = `${token}`;
    }
    return config;
  },
  (error) => {
    // do something
    return Promise.reject(error);
  }
);
// add response interceptors
axios.interceptors.response.use(
  (response: AxiosResponse<HttpResponse>) => {
    const res = response.data;
    if (res.statusCode !== 200) {
      let errorMsg = '';
      if (isString(res.errors)) {
        errorMsg = res.errors;
      } else {
        errorMsg = JSON.stringify(res.errors) || 'Error';
      }
      Message.error({
        content: errorMsg,
        duration: 5 * 1000,
      });
      // 50008: Illegal token; 50012: Other clients logged in; 50014: Token expired;
      if (
        [50008, 50012, 50014].includes(res.statusCode) &&
        response.config.url !== '/api/user/info'
      ) {
        Modal.error({
          title: 'Confirm logout',
          content:
            'You have been logged out, you can cancel to stay on this page, or log in again',
          okText: 'Re-Login',
          async onOk() {
            const userStore = useUserStore();

            await userStore.logout();
            window.location.reload();
          },
        });
      }
      return Promise.reject(new Error(errorMsg || 'Error'));
    }

    return res;
  },
  (error) => {
    const errorObj = JSON.parse(JSON.stringify(error));
    if (errorObj.status === 401) {
      Modal.error({
        title: 'Confirm logout',
        content:
          'You have been logged out, you can cancel to stay on this page, or log in again',
        okText: 'Re-Login',
        async onOk() {
          const userStore = useUserStore();
          await userStore.logout();
          window.location.reload();
        },
      });
    } else {
      Message.error({
        content: error.message || 'Request Error',
        duration: 5 * 1000,
      });
    }
    return Promise.reject(error);
  }
);
