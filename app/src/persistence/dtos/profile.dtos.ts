export interface ProfileDto {
    id: string,
    email: string,
    displayName: string,
    avatarUrl?: string | null,
    createdAtUtc: string,
    updatedAtUtc: string,
}
