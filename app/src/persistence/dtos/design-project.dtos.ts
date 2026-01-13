export interface DesignProjectDto {
    id: string
    identityId: string
    name: string
    descriptionPayload: string | null
    designTasks?: Array<DesignTaskDto> | null
    createdAt: string
    updatedAt: string
}

export interface DesignTaskDto {
    id: string
    projectId: string
    number: number
    name: string
    descriptionPayload: string
    status: number
    designs?: Array<DesignDto> | null
    createdAt: string
    updatedAt: string
}

export interface DesignDto {
    id: string
    taskId: string
    name: string
    descriptionPayload: string | null
    type: number
    payload: string
    createdAt: string
    updatedAt: string
}

export interface AddDesignProjectDto {
    name: string
    descriptionPayload: string | null
}

export interface AddDesignProjectResultDto {
    projectId: string
}
