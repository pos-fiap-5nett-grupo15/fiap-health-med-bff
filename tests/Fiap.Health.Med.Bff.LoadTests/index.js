import http from 'k6/http';
import { check, sleep } from 'k6';
import getDoctorByFilters from './UseCases/v1/Doctor/get-doctor-by-filters.js';
import getDoctorById from './UseCases/v1/Doctor/get-doctor-by-id.js';

// Define múltiplos cenários
export let options = {
    scenarios: {
        getDoctorByFilters: {
            executor: 'ramping-vus',
            exec: 'getDoctorByFilters',
            startVUs: 0,
            stages: [
                { duration: '10s', target: 5 }, // Sobe para 'N' usuários em 'N' tempo
                { duration: '20s', target: 5 }, // Mantém 'N' usuários por 'N' tempo
                { duration: '10s', target: 0 },  // Diminui para 0 usuários
            ],
        },
        getDoctorById: {
            executor: 'ramping-vus',
            exec: 'getDoctorById',
            startVUs: 0,
            stages: [
                { duration: '10s', target: 5 },
                { duration: '20s', target: 5 },
                { duration: '10s', target: 0 },
            ],
        },
    },
};

export { getDoctorByFilters, getDoctorById };
