import http from 'k6/http';
import { check, sleep, fail } from 'k6';
import doctorAuth from './UseCases/v1/doctorAuth.js';

export default function () {
    let headers = {
        'Authorization': doctorAuth,
        'Content-Type': 'application/json',
        'Content-Type': 'application/json',
    };
    let res = http.get(`http://135.237.26.77/Doctor/1`, { headers });

    if (
        !check(res, {
            'status é 200': (r) => r.status === 200,
            //'tempo de resposta < 500ms': (r) => r.timings.duration < 500,
        })
    ) {
        fail(`get-doctor-by-filters - K6 Check - Fail: status code recebido - ${res.status}`);
    }

    sleep(1); // pausa de 1s entre as execuções
}