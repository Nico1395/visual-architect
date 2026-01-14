import http from "@/http"
import type {
    AddDesignProjectDto,
    AddDesignProjectResultDto,
    AddDesignTaskDtoV1,
    AddDesignTaskResultDtoV1,
    DesignProjectDto,
    UpdateDesignProjectDto
} from "../dtos/design-project.dtos";

export async function getProjectById(projectId: string) {
    const { data } = await http.get<DesignProjectDto>(`/api/v1/app-design/projects/${projectId}?incltsk=true&incldsg=true`)
    return data;
}

export async function getOwnedProjects() {
    const { data } = await http.get<Array<DesignProjectDto>>("/api/v1/app-design/projects/owned?incltsk=true&incldsg=true")
    return data;
}

export async function addProject(contract: AddDesignProjectDto) {
    const { data } = await http.post<AddDesignProjectResultDto>("/api/v1/app-design/projects/add", {
        name: contract.name,
        descriptionPayload: contract.descriptionPayload,
    })

    return data;
}

export async function updateProject(contract: UpdateDesignProjectDto) {
    await http.patch("/api/v1/app-design/projects/update", {
        id: contract.id,
        name: contract.name,
        descriptionPayload: contract.descriptionPayload,
    })
}

export async function addTask(contract: AddDesignTaskDtoV1) {
    const { data } = await http.post<AddDesignTaskResultDtoV1>("/api/v1/app-design/tasks/add", {
        projectId: contract.projectId,
        name: contract.name,
        descriptionPayload: contract.descriptionPayload,
    })

    return data;
}
