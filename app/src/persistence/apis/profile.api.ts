import http from "@/http";
import type { ProfileDto } from "../dtos/profile.dtos";

export async function getProfile() {
    const { data } = await http.get<ProfileDto>(`/api/profile`)
    return data
}
