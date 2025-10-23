## Summary of compilation errors

The file `BussinessCore\API\Controllers\PSP\EntitiesController.cs` has a syntax error on line 749.

**Error:** 
```
error CS1513: Se esperaba }
```

**Root Cause:**
The file is missing closing braces for:
1. The `EntitiesController` class  
2. The `SmartClickCore.API.Controllers.PSP` namespace

**Fix Required:**
Add the following two lines at the very end of the file (after line ~748):

```csharp
    } // Closes EntitiesController class
} // Closes namespace
```

The file currently ends with commented code but no closing braces.

**Solution:**
Manually add these two closing braces at the end of `BussinessCore\API\Controllers\PSP\EntitiesController.cs`.
