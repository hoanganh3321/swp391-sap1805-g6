export default function useAuth() {
    return localStorage.getItem("token") || "";
}

export  function removeToken() {
  localStorage.removeItem("token");
}