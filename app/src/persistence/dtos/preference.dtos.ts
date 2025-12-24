export interface PreferenceDto {
    identityId: string
    key: string,
    value?: string | null
    defaultValue?: string | null
    updatedAt?: string | null
}

export interface SetPreferenceDto {
    key: string
    value?: string | null
    resetToDefault: boolean
}
