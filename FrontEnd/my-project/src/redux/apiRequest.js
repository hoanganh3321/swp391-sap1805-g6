import axios from "axios";
import { loginFailed, loginStart, loginSuccess } from "./authSlice";
//npm install axios

export const loginUser = async (user, dispatch, navigate) => {
    dispatch(loginStart());
    try {
        const res = await axios.post("https://localhost:7002/api/admin/login", user);
        dispatch(loginSuccess(res.data));
        navigate("/")
    } catch (error) {
        dispatch(loginFailed());
    }
}

