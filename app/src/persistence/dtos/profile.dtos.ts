export interface ProfileDtoV1 {
    id: string,
    email: string,
    displayName: string,
    avatarUrl?: string | null,
    createdAt: string,
    updatedAt: string,
}

export interface UpdateProfileDtoV1 {
    email: string,
    displayName: string,
    avatarUrl?: string | null,
}
