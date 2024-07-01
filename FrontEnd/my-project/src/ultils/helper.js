import { commonAPI } from "../api/common.api"


export function isTokenExpired(token) {
    // Tách phần payload của token từ phần còn lại bằng cách tách chuỗi bằng dấu chấm
    const tokenParts = token?.split(".");
    const payload = JSON.parse(atob(tokenParts[1]));

    // Lấy thời gian hết hạn (exp) từ payload
    const expirationTime = payload?.exp;
    console.log(expirationTime);

    // Lấy thời gian hiện tại (tính bằng giây)
    const currentTime = Math.floor(Date.now() / 1000);

    // So sánh thời gian hết hạn với thời gian hiện tại
    return expirationTime < currentTime;
}

export const logout = async (isRedirect = true) => {
    let pathRedirect = "/login";
    
    const roleApp = localStorage.getItem("roleApp");
    var res = null;
    if (roleApp == "Admin") {
        pathRedirect = "/loginAdmin";
        res = await commonAPI.adminLogout();
    }else {
        res = await commonAPI.staffLogout();
    }
    localStorage.removeItem("token");
    localStorage.removeItem("roleApp");
    if (isRedirect) {
        window.location.href = pathRedirect;
    }
};