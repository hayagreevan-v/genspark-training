import { Component, Input, input } from '@angular/core';
import { RecipeModel } from '../models/RecipeModel';

@Component({
  selector: 'app-recipe',
  imports: [],
  templateUrl: './recipe.html',
  styleUrl: './recipe.css'
})
export class Recipe {
  @Input() recipe : RecipeModel|null = new RecipeModel();

}
