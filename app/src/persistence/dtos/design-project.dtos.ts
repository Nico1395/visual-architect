export interface DesignProjectDtoV1 {
    id: string
    identityId: string
    name: string
    descriptionPayload: string | null
    designTasks?: Array<DesignTaskDtoV1> | null
    createdAt: string
    updatedAt: string
}

export interface DesignTaskDtoV1 {
    id: string
    projectId: string
    number: number
    name: string
    descriptionPayload: string
    status: number
    designs?: Array<DesignDtoV1> | null
    createdAt: string
    updatedAt: string
}

export interface DesignDtoV1 {
    id: string
    taskId: string
    name: string
    descriptionPayload: string | null
    type: number
    payload: string
    createdAt: string
    updatedAt: string
}

export interface AddDesignProjectDtoV1 {
    name: string
    descriptionPayload: string | null
}

export interface AddDesignProjectResultDtoV1 {
    projectId: string
}

export interface UpdateDesignProjectDtoV1 {
    id: string
    name: string
    descriptionPayload: string | null
}

export interface AddDesignTaskDtoV1 {
    name: string
    descriptionPayload: string
}

export interface AddDesignTaskResultDtoV1 {
    taskNumber: number
}

export interface UpdateDesignTaskDtoV1 {
    name: string
    descriptionPayload: string
    status: number
}

// 0 -> code
// 1 -> mermaid
// 2 -> plantuml
export function getDesignTypes() {
    return [0, 1, 2];
}

export interface AddDesignDtoV1 {
    name: string
    descriptionPayload: string
    type: number
}

export interface AddDesignResultDtoV1 {
    designId: string
}
