import axios from "axios";
import { loginFailed, loginStart, loginSuccess } from "./authSlice";

export const loginUser = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    const res = await axios.post("https://localhost:7002/api/admin/login", user);
    const token = res.data.token; // Assuming the token is in res.data.token

    // Log the token to the console
    console.log("JWT Token:", token);

    // Store the token in localStorage
    localStorage.setItem("jwtToken", token);

    // Optionally, set the token as a default header for future requests
    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    dispatch(loginSuccess(res.data));
    navigate("/home");
  } catch (error) {
    dispatch(loginFailed());
  }
};
