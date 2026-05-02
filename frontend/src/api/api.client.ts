import axios from 'axios';
import { Configuration } from './configuration';
import { WeatherForecastApi } from './api';

export default class ApiClient {
    public weatherForecast: WeatherForecastApi;

    constructor(baseUrl: string = import.meta.env.API_URL || 'http://localhost:5152') {
        const configuration = new Configuration({
            basePath: baseUrl,
            baseOptions: {
                withCredentials: true,
            },
        });

        const axiosInstance = axios.create({
            baseURL: baseUrl,
            withCredentials: true,
        });

        this.weatherForecast = new WeatherForecastApi(configuration, baseUrl, axiosInstance);
    }
}
