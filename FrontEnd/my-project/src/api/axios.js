import axios from "axios";

const baseURL = `https://localhost:7002/api/product/`;

export default axios.create({
  baseURL: baseURL,
  headers: {
    "Content-Type": "application/json",
  },
});
