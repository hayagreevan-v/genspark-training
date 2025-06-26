import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Recipe } from './recipe';

describe('Recipe', () => {
  let component: Recipe;
  let fixture: ComponentFixture<Recipe>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Recipe]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Recipe);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should display',() =>{
      component.recipe = {
        name:'Abc',
        cuisine:'blah blah',
        cookTimeMinutes: 15,
        image: '',
        ingredients: []
      }
      fixture.detectChanges();
      expect((fixture.nativeElement as HTMLElement).textContent).toContain("Abc");
  })
});
