import http from "@/http";
import type { ProfileDto } from "../dtos/profile.dtos";

export async function getProfile() {
    const { data } = await http.get<ProfileDto>(`/api/profile`)
    return data
}

export async function saveProfile(profile: ProfileDto) {
    const payload = {
        id: profile.id,
        email: profile.email,
        displayName: profile.displayName,
        avatarUrl: profile.avatarUrl ?? null,
        createdAt: profile.createdAt,
        updatedAt: profile.updatedAt,
    };

    await http.patch(`/api/profile/save`, payload);
}

export async function deleteProfile() {
    await http.delete(`/api/profile/delete`);
}
