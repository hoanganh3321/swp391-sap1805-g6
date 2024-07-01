import axios from 'axios';
import { isTokenExpired, logout } from "../ultils/helper.js";
import { toast } from "react-toastify";
const baseURL = `https://localhost:7002/api/`;
export  const API = axios.create({
  baseURL: baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
});

API.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token"); 
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    const token = localStorage.getItem("token");
    
    if (isTokenExpired(token)) {
      toast.error("Phiên đăng nhập đã hết hạn",{
        onClose:() =>  logout()
      })
      
    }
   else{
    return Promise.reject(error);
   }
  }
);
API.interceptors.response.use(
  response => response,
  error => {
    const token = localStorage.getItem("token");
    
    if (token && isTokenExpired(token)) {
      toast.error("Phiên đăng nhập đã hết hạn",{
        onClose:() =>  logout()
      })
      
    }
    else{
      return Promise.reject(error);
    }
  }
 
);

