import { Component, OnInit, inject } from '@angular/core';
import { QuizService, QuizSummary } from './services/quize.service';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private readonly quizService = inject(QuizService);

  quizzes: QuizSummary[] = [];
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
