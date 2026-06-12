# Angular API Call Plan: Show Quiz List

This plan explains how to call the Web API from the Angular frontend and render the quiz list returned by `GET /api/quiz`.

## Goal

Show quiz names from the API in the Angular app.

API endpoint:

```text
http://localhost:5182/api/quiz
```

Expected response:

```json
[
  "Angular Fundamentals",
  "C# Basics",
  "ASP.NET Core Web API",
  "TypeScript Essentials",
  "HTML and CSS"
]
```

## Step 1: Enable HTTP Client

Edit:

```text
angular/frontend/src/app/app.config.ts
```

Import `provideHttpClient`:

```ts
import { provideHttpClient } from '@angular/common/http';
```

Add it to the providers:

```ts
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideHttpClient()
  ]
};
```

This makes Angular `HttpClient` available to the app.

## Step 2: Put API Logic in a Service

For learning, the HTTP call can be written directly in `AppComponent`.

The better Angular pattern is to create a service:

```text
angular/frontend/src/app/services/quiz.service.ts
```

The service should own the API call. The component should own display state.

Recommended responsibility split:

```text
QuizService
  calls GET http://localhost:5182/api/quiz

AppComponent
  asks QuizService for quizzes
  stores quiz names in a field
  template renders the list
```

Example service:

```ts
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5182/api/quiz';

  getQuizzes(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrl);
  }
}
```

## Step 3: Use the Service in AppComponent

Edit:

```text
angular/frontend/src/app/app.component.ts
```

Example component logic:

```ts
import { Component, OnInit, inject } from '@angular/core';
import { QuizService } from './services/quiz.service';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private readonly quizService = inject(QuizService);

  quizzes: string[] = [];
  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadQuizzes();
  }

  loadQuizzes(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.quizService.getQuizzes().subscribe({
      next: quizzes => {
        this.quizzes = quizzes;
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Could not load quizzes.';
        this.isLoading = false;
      }
    });
  }
}
```

## Step 4: Render the List with `@for`

Edit:

```text
angular/frontend/src/app/app.component.html
```

Example template:

```html
<main>
  <h1>Quizzes</h1>

  @if (isLoading) {
    <p>Loading quizzes...</p>
  }

  @if (errorMessage) {
    <p>{{ errorMessage }}</p>
  }

  <ul>
    @for (quiz of quizzes; track quiz) {
      <li>{{ quiz }}</li>
    }
  </ul>
</main>
```

Important: `@for` is written in the HTML template, not in `component.ts`.

In Angular 19, `@for` and `@if` are built-in template control flow features. You do not need to import a directive to use them.

## Older Alternative: `*ngFor`

If using older Angular syntax, import `CommonModule` in the component:

```ts
import { CommonModule } from '@angular/common';

@Component({
  imports: [CommonModule]
})
```

Then use:

```html
<li *ngFor="let quiz of quizzes">{{ quiz }}</li>
```

For this project, prefer Angular 19 `@for`.

## Step 5: Run Both Projects

Terminal 1:

```powershell
cd C:\tmp\angular\web.api
dotnet run
```

Terminal 2:

```powershell
cd C:\tmp\angular\frontend
npm start
```

Open:

```text
http://localhost:4200
```

The frontend should call:

```text
http://localhost:5182/api/quiz
```
