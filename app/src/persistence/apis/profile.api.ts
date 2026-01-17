import http from "@/http";
import type { ProfileDtoV1, UpdateProfileDtoV1 } from "../dtos/profile.dtos";

export async function getProfile() {
    const { data } = await http.get<ProfileDtoV1>(`/api/v1/profile`)
    return data
}

export async function saveProfile(contract: UpdateProfileDtoV1) {
    await http.patch(`/api/v1/profile/save`, {
        email: contract.email,
        displayName: contract.displayName,
        avatarUrl: contract.avatarUrl,
    });
}

export async function deleteProfile() {
    await http.delete(`/api/v1/profile/delete`);
}
