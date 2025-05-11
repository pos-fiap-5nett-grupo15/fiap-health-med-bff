import http from 'k6/http';
import { check, sleep, fail } from 'k6';
import doctorAuth from './UseCases/v1/doctorAuth.js';

export default function () {
    let headers = {
        'Authorization': doctorAuth,
        'Content-Type': 'application/json',
    };

    let currentDate = new Date();
    currentDate.setDate(currentDate.getDate() + 2);

    let body = {
        'doctorId': 2,
        'scheduleTime': currentDate.toISOString(),
        'price': 100
    };

    let res = http.post('http://135.237.26.77/Schedule', body, headers);

    if (!check(res, {
            'status é 200 ou 400': (r) => r.status === 200 ,
            'tempo de resposta < 500ms': (r) => r.timings.duration < 500,
    })) {
        fail(`post-create-schedule - K6 Check - Fail: status code recebido - ${res.status} - request body ${JSON.stringify(body)} - response body: ${JSON.stringify(res.body)} and authorization: ${authorization}`);
    }

    sleep(1); // pausa de 1s entre as execuções
}