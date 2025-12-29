export interface DesignProjectDto {
    id: string
    identityId: string
    name: string
    descriptionPayload: string | null
    createdAt: string
    updatedAt: string
}

export interface AddDesignProjectDto {
    name: string
    descriptionPayload: string | null
}
