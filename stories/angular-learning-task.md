# Angular Learning Task: Quiz Selection and First Question Flow

## Goal

Build the first frontend slice of the quiz application described in `quiz_user_stories_with_ac.txt`.

This task focuses on Angular basics: standalone components, templates, property binding, event binding, `@for`, `@if`, component state, and simple TypeScript models.

## User Stories Covered

- US-039: Display list of available quizzes
- US-040: Select quiz from list
- US-041: Navigate to quiz page after selection
- US-024: Create session when user enters quiz page
- US-031: Restart quiz session

## What To Build

Create a simple in-memory quiz UI in `angular/frontend`.

The app should start on a quiz selection screen. A user can select a quiz, see the first question, select an answer, and restart the quiz.

No backend integration is required for this task. Use hardcoded data in the Angular app.

## Suggested Files

You can keep this as a single-component exercise first:

- `src/app/app.component.ts`
- `src/app/app.component.html`
- `src/app/app.component.css`

After it works, optionally refactor into smaller components:

- `quiz-list.component.ts`
- `quiz-page.component.ts`
- `question-card.component.ts`

## Data Model

Create TypeScript interfaces like this:

```ts
interface Quiz {
  id: string;
  title: string;
  description: string;
  questions: Question[];
}

interface Question {
  id: string;
  text: string;
  answers: Answer[];
}

interface Answer {
  id: string;
  text: string;
  isCorrect: boolean;
}

interface QuizSession {
  id: string;
  quizId: string;
  currentQuestionIndex: number;
  selectedAnswerId?: string;
}
```

## Requirements

1. Display at least 2 quizzes on the start screen.
2. Each quiz must show a title and short description.
3. Clicking a quiz creates a new `QuizSession`.
4. The quiz page shows:
   - selected quiz title
   - generated session ID
   - current question text
   - all answer options
5. Clicking an answer stores the selected answer ID in the session.
6. The selected answer should be visually highlighted.
7. Add a `Restart` button.
8. Restart clears the selected answer and starts again from the first question.
9. Add a `Back to quizzes` button that returns to quiz selection.

## Acceptance Criteria

- Given the app is opened, when no quiz is selected, then the quiz list is visible.
- Given the quiz list is visible, when a quiz is clicked, then a session is created with a unique ID.
- Given a quiz session exists, when the quiz page renders, then the first question is displayed.
- Given an answer is clicked, then the selected answer is stored and highlighted.
- Given `Restart` is clicked, then the first question is shown and no answer is selected.
- Given `Back to quizzes` is clicked, then the quiz selection screen is shown again.

## Angular Concepts To Practice

- `@Component`
- component fields and methods
- TypeScript interfaces
- `@if` conditional rendering
- `@for` list rendering
- `[class.selected]` class binding
- `(click)` event binding
- `ngClass` or direct class bindings
- simple state transitions without routing

## Stretch Goals

- Add `Next question` and `Previous question` buttons.
- Track selected answers for every question.
- Show progress like `Question 1 of 5`.
- Disable `Next question` until an answer is selected.
- Show a final score after the last question.
- Move quiz data into a separate `quiz-data.ts` file.
- Create a `QuizService` that returns quizzes from memory.

## Done When

Run these commands from `angular/frontend`:

```bash
npm install
npm run build
```

The build should complete without TypeScript or template errors.
