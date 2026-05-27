import axios from 'axios';
import { Configuration } from './configuration';
import { MentorsApi, ProjectsApi, StudentsApi, TeamsApi, DataApi } from './api';

export default class ApiClient {
    public mentors: MentorsApi;
    public projects: ProjectsApi;
    public students: StudentsApi;
    public teams: TeamsApi;
    public data: DataApi;

    constructor(baseUrl: string = import.meta.env.VITE_API_URL || 'http://localhost:5152') {
        const configuration = new Configuration({
            basePath: baseUrl,
        });

        const axiosInstance = axios.create({
            baseURL: baseUrl,
        });
        
        this.mentors = new MentorsApi(configuration, baseUrl, axiosInstance);
        this.projects = new ProjectsApi(configuration, baseUrl, axiosInstance);
        this.students = new StudentsApi(configuration, baseUrl, axiosInstance);
        this.teams = new TeamsApi(configuration, baseUrl, axiosInstance);
        this.data = new DataApi(configuration, baseUrl, axiosInstance);
    }
}
