import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface QuizSummary {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5182/api/quiz';

  getQuizzes(): Observable<QuizSummary[]> {
    return this.http.get<QuizSummary[]>(this.apiUrl);
  }
}
