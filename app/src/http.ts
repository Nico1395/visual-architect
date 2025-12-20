import axios from "axios";

const http = axios.create({
  baseURL: "https://localhost:7050",
  withCredentials: true,
});

http.interceptors.request.use(async (config) => {
  if (config.method !== "get") {
    const token = sessionStorage.getItem("vat") ?? (await fetchCsrfToken());
    config.headers["X-CSRF-TOKEN"] = token;
  }
  return config;
});

async function fetchCsrfToken() {
  const { data } = await http.get("/api/auth/csrf");
  sessionStorage.setItem("vat", data.token);
  return data.token;
}

export default http
