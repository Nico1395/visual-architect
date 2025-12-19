import axios from "axios";

export const http = axios.create({
  baseURL: "https://localhost:7050",
  withCredentials: true,
});
