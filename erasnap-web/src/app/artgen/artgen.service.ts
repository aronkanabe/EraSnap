import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenderPrompt, GeneratedImage, Prompt } from './artgen.model';
import { WebcamImage } from 'ngx-webcam';

@Injectable({ providedIn: 'root' })
export class ArtgenService {

  constructor(protected http: HttpClient) {}

  queryPrompts(): Observable<HttpResponse<Prompt[]>> {
    return this.http.get<Prompt[]>('https://erasnap-container-app.yellowsmoke-b696d1e5.northeurope.azurecontainerapps.io/prompts', {observe: 'response' });
  }

  findGeneratedImage(imageId: string): Observable<HttpResponse<GeneratedImage>> {
    return this.http.get<GeneratedImage>(`https://erasnap-container-app.yellowsmoke-b696d1e5.northeurope.azurecontainerapps.io/images/${imageId}`, {observe: 'response' });
  }

  generateImage(genderPrompt: GenderPrompt, portrait: WebcamImage): Observable<HttpResponse<GeneratedImage>> {
    return this.http.post<GeneratedImage>('https://erasnap-container-app.yellowsmoke-b696d1e5.northeurope.azurecontainerapps.io/image', {image: portrait.imageAsBase64, promptId: genderPrompt.prompt?.id, gender: genderPrompt.gender}, { observe: 'response' });
  }

}
