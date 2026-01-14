import http from "@/http";
import type { ProfileDtoV1 } from "../dtos/profile.dtos";

export async function getProfile() {
    const { data } = await http.get<ProfileDtoV1>(`/api/v1/profile`)
    return data
}

export async function saveProfile(profile: ProfileDtoV1) {
    await http.patch(`/api/v1/profile/save`, {
        id: profile.id,
        email: profile.email,
        displayName: profile.displayName,
        avatarUrl: profile.avatarUrl ?? null,
        createdAt: profile.createdAt,
        updatedAt: profile.updatedAt,
    });
}

export async function deleteProfile() {
    await http.delete(`/api/v1/profile/delete`);
}
