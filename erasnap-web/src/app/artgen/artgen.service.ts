import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Prompt } from './artgen.model';

@Injectable({ providedIn: 'root' })
export class ArtgenService {

  constructor(protected http: HttpClient) {}

  query(): Observable<HttpResponse<Prompt[]>> {
    return this.http.get<Prompt[]>('https://erasnap-container-app.yellowsmoke-b696d1e5.northeurope.azurecontainerapps.io/prompts', {observe: 'response' });
  }

}
