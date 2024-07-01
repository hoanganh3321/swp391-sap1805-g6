import { createSlice } from "@reduxjs/toolkit";
import { removeToken } from "../hook/useAth";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const authSlice = createSlice({
    name: "auth",
    initialState: {
        login: {
            currenUser: null,
            isFetching: false,
            error: false,
        }
    },
    reducers: {
        loginStart: (state) => {
            removeToken();
            state.login.isFetching = true;
        },
        loginSuccess: (state, action) => {
            state.login.isFetching = false;
            state.login.currenUser = action.payload;
            state.login.error = false;
        },
        loginFailed: (state) => {
            
            state.login.isFetching = false;
            state.login.error = true;
            toast.error("Failed to login",{});
        }
    }

});
export const {
    loginSuccess,
    loginStart,
    loginFailed,

} = authSlice.actions;

export default authSlice.reducer