import { TestBed } from "@angular/core/testing"
import { RecipeService } from "./RecipeService"
import { provideHttpClient } from "@angular/common/http"
import { HttpTestingController, provideHttpClientTesting } from "@angular/common/http/testing"

describe('RecipeService', () => {
    let service : RecipeService;
    let httpMock : HttpTestingController;
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports : [],
            providers : [RecipeService, provideHttpClient(), provideHttpClientTesting()]
        });
        service = TestBed.inject(RecipeService);
        httpMock = TestBed.inject(HttpTestingController);
    })

    afterEach(()=>{
        httpMock.verify();
    })
    it('should retrieve recipes from API',() => {
        const mockRecipe = {
            name:'Abc',
            cuisine:'blah blah',
            cookTimeMinutes: 15,
            image: '',
            ingredients: []
        };
        service.getRecipes().subscribe((recipe : any) => {
            expect(recipe).toBe(mockRecipe);
        })
        const req = httpMock.expectOne('https://dummyjson.com/recipes');
        expect(req.request.method).toBe('GET');
        req.flush(mockRecipe);
    })
})