CREATE OR REPLACE FUNCTION fn_GetDoctorsBySpeciality(speciality VARCHAR(20))
RETURNS TABLE(id int, dname text, yoe real)
AS $$
BEGIN
	RETURN QUERY SELECT DISTINCT "Id","Name","YearsOfExperience" FROM doctors WHERE "Id" in 
		(SELECT "DoctorId" FROM "doctorSpecialities" WHERE "SpecialityId" IN
			(SELECT "Id" FROM specialities WHERE "Name" = speciality)
		);
END;
$$
LANGUAGE PLPGSQL;

SELECT * FROM fn_GetDoctorsBySpeciality('Neurology');