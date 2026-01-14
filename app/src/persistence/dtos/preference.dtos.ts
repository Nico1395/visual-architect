export interface PreferenceDtoV1 {
    identityId: string
    key: string,
    value?: string | null
    defaultValue?: string | null
    updatedAt?: string | null
}

export interface SetPreferenceDtoV1 {
    key: string
    value?: string | null
    resetToDefault: boolean
}
