import http from "@/http";
import router from "@/router";

export const gitHubKey = "github";
export const googleKey = "google";
export const msKey = "microsoft";

export async function isAuthenticated(): Promise<boolean> {
    try {
        await http.get("/api/auth/me");
        return true;
    } catch {
        return false;
    }
}

export async function authenticate(returnUri?: string | null) {
    const authenticated = await isAuthenticated();
    if (authenticated) {
        router.replace(returnUri ?? "/app/home");
    } else {
        router.replace({
            path: "/auth/login",
            query: returnUri ? { returnUri } : undefined,
        });
    }
}
