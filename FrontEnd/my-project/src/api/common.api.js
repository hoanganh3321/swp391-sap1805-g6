import { API } from ".";

export const commonAPI = {
  adminLogout: () => {
    return API.post("admin/logout", {});
  },
  staffLogout: () => {
    return API.post("staff/logout", {});
  },
  postAPI: (path, param) => {
    return API.post(path, param);
  },
  getAPI: (path) => {
    return API.get(path);
  },
  putAPI: (path, param) => {
    return API.put(path, param);
  },
  deleteAPI: (path, param) => {
    return API.delete(path, param);
  }

};
