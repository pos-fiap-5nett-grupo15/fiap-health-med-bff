import http from 'k6/http';
import { check, sleep } from 'k6';
import getDoctorByFilters from './UseCases/v1/Doctor/get-doctor-by-filters.js';
import getDoctorById from './UseCases/v1/Doctor/get-doctor-by-id.js';
import getScheduleByDoctorId from './UseCases/v1/Schedule/get-schedule-by-doctor-id.js';

// Define múltiplos cenários
export let options = {
    scenarios: {
        getDoctorByFilters: {
            executor: 'ramping-vus',
            exec: 'getDoctorByFilters',
            startVUs: 0,
            stages: [
                { duration: '1m', target: 100 },    // Sobe para 'N' usuários em 'N' tempo
                { duration: '30s', target: 100 },   // Mantém 'N' usuários por 'N' tempo
                { duration: '10s', target: 0 },     // Diminui para 0 usuários
            ],
        },
        getDoctorById: {
            executor: 'ramping-vus',
            exec: 'getDoctorById',
            startVUs: 0,
            stages: [
                { duration: '1m', target: 100 },    // Sobe para 'N' usuários em 'N' tempo
                { duration: '30s', target: 100 },   // Mantém 'N' usuários por 'N' tempo
                { duration: '10s', target: 0 },     // Diminui para 0 usuários
            ],
        },
        getScheduleByDoctorId: {
            executor: 'ramping-vus',
            exec: 'getScheduleByDoctorId',
            startVUs: 0,
            stages: [
                { duration: '1m', target: 100 },    // Sobe para 'N' usuários em 'N' tempo
                { duration: '30s', target: 100 },   // Mantém 'N' usuários por 'N' tempo
                { duration: '10s', target: 0 },     // Diminui para 0 usuários
            ],
        },
    },
};

export { getDoctorByFilters, getDoctorById, getScheduleByDoctorId };
