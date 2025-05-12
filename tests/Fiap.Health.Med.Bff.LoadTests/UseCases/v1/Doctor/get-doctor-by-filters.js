import http from 'k6/http';
import { check, sleep, fail } from 'k6';


export default function () {
    let headers = {
        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InJqNzc3Iiwicm9sZSI6IkRvY3RvciIsIm5iZiI6MTc0NzAxMTM0OCwiZXhwIjoxNzQ3MDExNjQ4LCJpYXQiOjE3NDcwMTEzNDh9.b52j5z4lsA_fqXajBfeEm-gGAUmwTeYLG13q3albBBc',
        'Content-Type': 'application/json',
    };
    let basePathAPI = 'http://135.237.26.77/';
    let resourcePath = 'Doctor/filters';
    const res = http.get(`${basePathAPI}${resourcePath}`, { headers });

    if (
        !check(res, {
            'status code retornado pela API é 200': (r) => r.status === 200,
            //'tempo de resposta < 500ms': (r) => r.timings.duration < 500,
        })
    ) {
        fail(`get-doctor-by-filters - K6 Check - Fail: status code recebido - ${res.status}`);
    }

    sleep(1); // pausa de 1s entre as execuções
}
