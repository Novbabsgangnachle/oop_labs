namespace DefaultNamespace;

public class EducationalProgramBuilder
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public Dictionary<int, List<ISubject>> SubjectsBySemester { get; private set; } = new Dictionary<int, List<ISubject>>();

    public Collection<Guid> ProgramLeaders { get; private set; } = new Collection<Guid>();

    public EducationalProgram Build()
    {
        Validate();
        return new EducationalProgram(this);
    }

    public EducationalProgramBuilder SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        Name = name;
        return this;
    }

    public EducationalProgramBuilder AddProgramLeader(User leader)
    {
        if (ProgramLeaders.Contains(leader.Id))
            throw new ArgumentException("Leader is already part of the program.");
        ProgramLeaders.Add(leader.Id);
        return this;
    }

    public EducationalProgramBuilder RemoveProgramLeader(User leader)
    {
        if (!ProgramLeaders.Contains(leader.Id))
            throw new ArgumentException("Leader not found.");
        ProgramLeaders.Remove(leader.Id);
        return this;
    }

    public EducationalProgramBuilder SetSubjectsBySemester(Dictionary<int, List<ISubject>> subjects)
    {
        if (subjects == null || subjects.Count == 0)
            throw new ArgumentException("Subjects cannot be null or empty.");
        SubjectsBySemester = new Dictionary<int, List<ISubject>>(subjects);
        return this;
    }

    public EducationalProgramBuilder AddSubject(int semester, ISubject subject)
    {
        if (semester <= 0)
            throw new ArgumentException("Semester must be a positive integer.");

        if (!SubjectsBySemester.ContainsKey(semester))
            SubjectsBySemester[semester] = new List<ISubject>();

        if (SubjectsBySemester[semester].Any(s => s.Id == subject.Id))
            throw new ArgumentException("Subject already exists in this semester.");

        SubjectsBySemester[semester].Add(subject);
        return this;
    }

    public EducationalProgramBuilder RemoveSubject(int semester, Guid subjectId)
    {
        if (!SubjectsBySemester.ContainsKey(semester))
            throw new ArgumentException("Non-existent semester.");

        ISubject? subject = SubjectsBySemester[semester].FirstOrDefault(s => s.Id == subjectId);
        if (subject == null)
            throw new ArgumentException("Subject not found in the specified semester.");

        SubjectsBySemester[semester].Remove(subject);
        return this;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new InvalidOperationException("Name is required.");

        if (ProgramLeaders.Count == 0)
            throw new InvalidOperationException("At least one program leader is required.");

        if (SubjectsBySemester.Count == 0)
            throw new InvalidOperationException("At least one semester with subjects is required.");
    }
}