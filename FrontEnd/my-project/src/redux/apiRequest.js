import axios from "axios";
import { loginFailed, loginStart, loginSuccess } from "./authSlice";

export const loginUser = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    const res = await axios.post("https://localhost:7002/api/admin/login", user);

    if (res.data && res.data.token) {
      const token = res.data.token;
      console.log("JWT Token:", token);

      localStorage.setItem("token", token);

      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      dispatch(loginSuccess(res.data));
      navigate("/home");
      const storedToken = localStorage.getItem("token");
      console.log("Stored Token:", storedToken);
    } else {
      throw new Error("Token not found in response");
    }
  } catch (error) {
    console.error("Login error:", error);
    dispatch(loginFailed());
  }
};
