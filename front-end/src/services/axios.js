import axios from 'axios';
 


//Create an Axios instance
const apiClient = axios.create({
    baseURL:  import.meta.env.VITE_API_BASE_URL,
    timeout: 10000,
});
//  request interceptor to include the token in every request
apiClient.interceptors.request.use(
    (response) => response,
    (error) => {
      console.error("Global Axios error:", error);
      return Promise.reject(error);
    }
);
export default apiClient;