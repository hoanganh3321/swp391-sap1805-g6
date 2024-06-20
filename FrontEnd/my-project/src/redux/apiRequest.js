import axios from "axios";
import { loginFailed, loginStart, loginSuccess } from "./authSlice";

export const loginUser = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    const res = await axios.post("https://localhost:7002/api/admin/login", user);
    const token = res.data.token;
    
    console.log("JWT Token:", token);
    localStorage.setItem("jwttoken", token);

    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    dispatch(loginSuccess(res.data));
    navigate("/home");
  } catch (error) {
    dispatch(loginFailed());
  }
};
