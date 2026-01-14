import axios from "axios";

const http = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
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
  const { data } = await http.get("/api/v1/auth/csrf");
  sessionStorage.setItem("vat", data.token);
  return data.token;
}

export default http
