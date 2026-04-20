import axios from 'axios';
import { Configuration } from './configuration';
import { WeatherForecastApi } from './api';

const BASE_URL = 'http://localhost:5152';

const configuration = new Configuration({
    basePath: BASE_URL,
    baseOptions: {
        withCredentials: true,
    },
});

const axiosInstance = axios.create({
    baseURL: BASE_URL,
    withCredentials: true,
});

class ApiClient {
    public weatherForecast: WeatherForecastApi;

    constructor() {
        this.weatherForecast = new WeatherForecastApi(configuration, BASE_URL, axiosInstance);
    }
}

export default new ApiClient();