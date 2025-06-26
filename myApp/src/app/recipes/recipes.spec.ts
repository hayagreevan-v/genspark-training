import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Recipes } from './recipes';
import { RecipeService } from '../services/RecipeService';
import { of } from 'rxjs';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

// @Component({
//   selector: 'app-recipe',
//   template: ''
// })
// class MockRecipeComponent {}

describe('Recipes', () => {
  let component: Recipes;
  let fixture: ComponentFixture<Recipes>;
  let serviceSpy : jasmine.SpyObj<RecipeService>;
  let dummyRecipeData : any = {
    recipes : [{
        name:'Abc',
        cuisine:'blah blah',
        cookTimeMinutes: 15,
        image: '',
        ingredients: []
      }],
    total:30
  } ;

  beforeEach(async () => {
    let spy = jasmine.createSpyObj('RecipeService',['getRecipes']);


    await TestBed.configureTestingModule({
      imports: [Recipes, CommonModule],
      providers:[{provide: RecipeService, useValue : spy}],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
    .compileComponents();

    serviceSpy = TestBed.inject(RecipeService) as jasmine.SpyObj<RecipeService>;
    serviceSpy.getRecipes.and.returnValue(of(dummyRecipeData));
    fixture = TestBed.createComponent(Recipes);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should contain recipes', () => {
    expect(component.recipes().length).toBe(1);
  });
});
