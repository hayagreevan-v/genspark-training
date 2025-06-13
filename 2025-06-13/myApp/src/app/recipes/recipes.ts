import { Component, signal} from '@angular/core';
import { RecipeService } from '../services/RecipeService';
import { RecipeModel } from '../models/RecipeModel';
import { Recipe } from "../recipe/recipe";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-recipes',
  imports: [Recipe,CommonModule],
  templateUrl: './recipes.html',
  styleUrl: './recipes.css'
})
export class Recipes {
  // recipes : RecipeModel[] | null = null;
  recipes = signal<RecipeModel[]>([]);

  constructor(private recipeService :RecipeService) {
    this.recipeService.getRecipes().subscribe({
      next: (data:any) => {
        console.log(data);
        this.recipes.set(data.recipes);
      },
      error : (err) => {
        console.error(err.message);
      },
      complete : () => {
        console.log("Completed");
      }
    })
  }

}
