import axios from "axios";
import { loginFailed, loginStart, loginSuccess } from "./authSlice";

export const loginUser = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    const res = await axios.post("https://localhost:7002/api/staff/login", user);

    if (res.data && res.data.jwttoken) {
      const token = res.data.jwttoken;
      if (res.data.jwttoken.includes("Invalid")){
        dispatch(loginFailed());
        return;
      }

      localStorage.setItem("token", token);
      localStorage.setItem("roleApp", "Staff");
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      dispatch(loginSuccess(res.data));
      navigate("/home");
    } else {
      dispatch(loginFailed());
    }
  } catch (error) {
    console.error("Login error:", error);
    dispatch(loginFailed());
  }
};

export const loginAdmin = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    const res = await axios.post("https://localhost:7002/api/admin/login", user);

    if (res.data && res.data.token) {
      if (res.data.token.includes("Invalid")){
        dispatch(loginFailed());
        return;
      }
      const token = res.data.token;
      localStorage.setItem("token", token);
      localStorage.setItem("roleApp", "Admin");
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      dispatch(loginSuccess(res.data));
      navigate("/admin");
    } else {
      dispatch(loginFailed());
    }
  } catch (error) {
    dispatch(loginFailed());
  }
};
